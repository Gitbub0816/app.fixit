using FixItQC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Infrastructure.Persistence;

public sealed class FixItQcDbContext(DbContextOptions<FixItQcDbContext> options) : DbContext(options)
{
    public DbSet<ClientOrganization> Organizations => Set<ClientOrganization>();
    public DbSet<Region> Regions => Set<Region>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<UserProfile> Users => Set<UserProfile>();
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<DispatchRecord> DispatchRecords => Set<DispatchRecord>();
    public DbSet<DispatchFlight> DispatchFlights => Set<DispatchFlight>();
    public DbSet<InspectionRecord> InspectionRecords => Set<InspectionRecord>();
    public DbSet<InspectionTemplate> InspectionTemplates => Set<InspectionTemplate>();
    public DbSet<InspectionTemplateSection> InspectionTemplateSections => Set<InspectionTemplateSection>();
    public DbSet<InspectionTemplateItem> InspectionTemplateItems => Set<InspectionTemplateItem>();
    public DbSet<InspectionExecution> InspectionExecutions => Set<InspectionExecution>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<PmTemplate> PmTemplates => Set<PmTemplate>();
    public DbSet<PmSchedule> PmSchedules => Set<PmSchedule>();
    public DbSet<DamageRecord> DamageRecords => Set<DamageRecord>();
    public DbSet<RunningBalanceLedgerEvent> LedgerEvents => Set<RunningBalanceLedgerEvent>();
    public DbSet<SafetyBulletin> SafetyBulletins => Set<SafetyBulletin>();
    public DbSet<SafetyBulletinAcknowledgement> BulletinAcknowledgements => Set<SafetyBulletinAcknowledgement>();
    public DbSet<CommsMessage> CommsMessages => Set<CommsMessage>();
    public DbSet<IntegrationMessage> IntegrationMessages => Set<IntegrationMessage>();
    public DbSet<MappingProfile> MappingProfiles => Set<MappingProfile>();
    public DbSet<MappingRule> MappingRules => Set<MappingRule>();
    public DbSet<NormalizedFlightUpdate> NormalizedFlightUpdates => Set<NormalizedFlightUpdate>();
    public DbSet<KpiSnapshot> KpiSnapshots => Set<KpiSnapshot>();
    public DbSet<Commendation> Commendations => Set<Commendation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<IntegrationMessage>().HasIndex(x => new { x.SourceMessageId, x.SourceSystem }).IsUnique();
        modelBuilder.Entity<MappingRule>().HasIndex(x => new { x.ProfileId, x.ExternalField }).IsUnique();
        modelBuilder.Entity<RunningBalanceLedgerEvent>().HasIndex(x => new { x.EquipmentId, x.EventUtc });
        modelBuilder.Entity<InspectionTemplate>().HasIndex(x => new { x.Name, x.IsJigTemplate });
        modelBuilder.Entity<WorkOrder>().HasIndex(x => new { x.StationId, x.Status });
    }
}
