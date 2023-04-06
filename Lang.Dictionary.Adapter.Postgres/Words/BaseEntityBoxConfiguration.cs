using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lang.Dictionary.Adapter.Postgres.Words
{
    internal abstract class BaseEntityBoxConfiguration<TEntity, TKey, TContent> : IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBox<TKey, TContent>
        where TContent : class
    {
        protected abstract string TableName { get; }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName).HasKey(t => t.Id).HasName(TableName + "_id");
            builder.Property(d => d.Id).HasColumnName("id");
            builder.Property(d => d.Content).HasColumnName("content").HasColumnType("jsonb");
            AdditionalConfigure(builder);
        }

        protected virtual void AdditionalConfigure(EntityTypeBuilder<TEntity> builder)
        {

        }
    }
}
