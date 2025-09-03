using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Context
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================
            // BaseEntite Configuration
            // ============================
            modelBuilder.Entity<BaseEntite>(entity =>
            {
                entity.HasKey(e => e.Id);                 // کلید اصلی
                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.IsDelete)
                      .HasDefaultValue(false);
            });

            // ============================
            // Category Configuration (Self-Referencing)
            // ============================
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasOne(c => c.Parent)              // هر Category یک والد دارد
                      .WithMany(c => c.Children)         // والد چند فرزند دارد
                      .HasForeignKey(c => c.ParentId)    // کلید خارجی
                      .OnDelete(DeleteBehavior.Restrict); // جلوگیری از حذف والد وقتی فرزند دارد
            });

            // ============================
            // Book Configuration
            // ============================
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(b => b.Author)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(b => b.Borrow)
                      .HasDefaultValue(false);

                entity.HasOne(b => b.Category)            // هر کتاب یک دسته دارد
                      .WithMany()                         // کالکشن توی Category نداریم
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict); // جلوگیری از حذف دسته وقتی کتاب دارد
            });
        }

        internal async Task<List<object>> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
