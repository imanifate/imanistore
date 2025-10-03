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
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Borrowing> Borrowing { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           

            // ============================
            // Category Configuration (Self-Referencing)
            // ============================
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(u => u.Id);

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
                entity.HasKey(u => u.Id);
                entity.Property(b => b.Author)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(b =>b.Publisher)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(b => b.PublicationDate)
                .IsRequired();

                entity.HasOne(b => b.Category)            // هر کتاب یک دسته دارد
                      .WithMany()                         // 
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict); // جلوگیری از حذف دسته وقتی کتاب دارد
            });
            // ============================
            // User Configuration
            // ============================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasMany(u => u.borrowings)// هر جدول User با چند جدول  borrowing در ارتباطه
               .WithOne(b => b.User) // هر جدول borrowing  با یک جدول User در ارتباطه 
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserId);

               entity.Property(u => u.NationalCode)
                .IsRequired()
                .HasMaxLength(10);

                entity.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11);
               
            });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(500);

                entity.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

                entity.Property(u => u.ActiveCode)
                .HasMaxLength(50);

                entity.Property(u => u.IsActive)
                .HasDefaultValue(true);

                entity.Property(u => u.IsAdmin)
                .HasDefaultValue(false);

            });
            // ============================
            // Borrowing Configuration
            // ============================
            modelBuilder.Entity<Borrowing>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasIndex(b => new { b.BookId, b.UserId });

                entity.HasOne(b => b.Book)  // هر جدول borrowing با یک جدول book در ارتباط است
                .WithMany(bk => bk.borrowings) // هر جدول book  تعدادی جدول borrowings در ارتباظ است
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.User)  // هر جدول borrowing با یک جدول  User در ارتباط است
                .WithMany(u => u.borrowings)  // هر جدول User  تعدادی جدول borrowings در ارتباظ است
                .HasForeignKey(b => b.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

                entity.Property(b => b.IsReturn)
                .HasDefaultValue(false);

                entity.Property(b => b.ReturnDate)
                .HasDefaultValue(null);
            });
        }


    }
}
