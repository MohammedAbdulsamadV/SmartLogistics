using Logistics.Domain.Common;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.Infrastucture.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IMediator mediator) : base(options)
    {
        _mediator = mediator;   
    }
    
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<ShipmentLeg> ShipmentLegs => Set<ShipmentLeg>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker.Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }

        foreach (var entity in domainEntities)
        {
            entity.ClearDomainEvents();
        }

        return result;
    }
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);

       
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey("OrderId") 
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.CustomerId).IsRequired();

        builder.OwnsOne(o => o.ShippingAddress);

        var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.VolumetricWeight).HasColumnType("decimal(18,2)");
        builder.Property(s => s.Status).HasConversion<int>().IsRequired();
        builder.Property(s => s.RoutePolyline).HasMaxLength(2000).IsRequired(false);

        builder.OwnsOne(s => s.CurrentLocation, cl =>
        {
            cl.Property(c => c.Latitude).HasColumnName("CurrentLatitude").HasColumnType("decimal(18,7)");
            cl.Property(c => c.Longitude).HasColumnName("CurrentLongitude").HasColumnType("decimal(18,7)");
        });

        builder.HasMany(s => s.ShipmentLegs)
            .WithOne()
            .HasForeignKey(l => l.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);
               
        var navigation = builder.Metadata.FindNavigation(nameof(Shipment.ShipmentLegs));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    public void Configure(EntityTypeBuilder<ChatRoom> builder)
    {
        builder.ToTable("ChatRooms");
        builder.HasKey(cr => cr.Id);
        
        builder.Property(cr => cr.OrderId).IsRequired();
        builder.Property(cr => cr.IsActive).IsRequired();

        builder.HasOne(cr => cr.Customer)
            .WithMany()
            .HasForeignKey(cr => cr.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(cr => cr.Messages)
            .WithOne()
            .HasForeignKey(m => m.ChatRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(ChatRoom.Messages))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Status).HasConversion<int>().IsRequired();
        builder.Property(p => p.Method).HasConversion<int>().IsRequired();

        builder.OwnsOne(p => p.GatewayDetails, gd =>
        {
            gd.Property(g => g.Provider).HasColumnName("GatewayProvider").HasMaxLength(50);
            gd.Property(g => g.Reference).HasColumnName("GatewayReference").HasMaxLength(250);
            gd.Property(g => g.CheckoutUrl).HasColumnName("GatewayCheckoutUrl").HasMaxLength(1000);
        });
    }

    public class ResponsibleConfiguration : IEntityTypeConfiguration<Responsible>
    {
        public void Configure(EntityTypeBuilder<Responsible> builder)
        {
            builder.ToTable("Responsibles");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name).HasMaxLength(200).IsRequired();
            builder.Property(r => r.Type).HasConversion<int>().IsRequired();
            builder.Property(r => r.ContactPhone).HasMaxLength(50).IsRequired();
            builder.Property(r => r.Email).HasMaxLength(150).IsRequired();
            builder.Property(r => r.IsActive).IsRequired(false);  
            builder.Property(r => r.TaxNumber).HasMaxLength(100).IsRequired(false);

            builder.OwnsOne(r => r.Integration);
        }
    }
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(150).IsRequired();
        builder.Property(c => c.PhoneNumber).HasMaxLength(50);
        builder.Property(c => c.TaxNumber).HasMaxLength(100);
        builder.Property(c => c.Type).HasConversion<int>().IsRequired();
        builder.Property(c => c.CreditLimit).HasColumnType("decimal(18,2)");
        builder.Property(c => c.CurrentBalance).HasColumnType("decimal(18,2)");
    }
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessages");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.SenderId).HasMaxLength(100).IsRequired();
        builder.Property(m => m.SenderType).HasConversion<int>().IsRequired();
        builder.Property(m => m.MessageText).HasMaxLength(2000).IsRequired();
        builder.Property(m => m.SentAt).IsRequired();
        builder.Property(m => m.IsRead).IsRequired();
    }
}