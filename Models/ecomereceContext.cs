using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ecommerece.Models
{
    public partial class ecomereceContext : DbContext
    {
        public ecomereceContext()
        {
        }

        public ecomereceContext(DbContextOptions<ecomereceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderLine> OrderLines { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Ratting> Rattings { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<Seller> Sellers { get; set; } = null!;
        public virtual DbSet<Subscriber> Subscribers { get; set; } = null!;
        public virtual DbSet<UserAdmin> UserAdmins { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MALIKFAZAL-PC\\SQLEXPRESS;Database=ecomerece;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Address1)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .HasColumnName("remarks");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId);

                entity.ToTable("category");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.CatDesc)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("cat_desc");

                entity.Property(e => e.CatImage)
                    .HasMaxLength(200)
                    .HasColumnName("cat_image");

                entity.Property(e => e.CatName)
                    .HasMaxLength(50)
                    .HasColumnName("cat_name");

                entity.Property(e => e.CatStatus).HasColumnName("cat_status");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.MetaData).HasColumnName("meta_data");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.SeoData).HasColumnName("seo_data");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.ToTable("customer");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("(((1)/(1))/(1900))");

                entity.Property(e => e.CustAddress)
                    .HasMaxLength(250)
                    .HasColumnName("cust_address");

                entity.Property(e => e.CustCity)
                    .HasMaxLength(20)
                    .HasColumnName("cust_city");

                entity.Property(e => e.CustDob)
                    .HasColumnType("datetime")
                    .HasColumnName("cust_dob")
                    .HasDefaultValueSql("(((1)/(1))/(1900))");

                entity.Property(e => e.CustEmail)
                    .HasMaxLength(40)
                    .HasColumnName("cust_email");

                entity.Property(e => e.CustGender)
                    .HasMaxLength(10)
                    .HasColumnName("cust_gender")
                    .IsFixedLength();

                entity.Property(e => e.CustImg)
                    .HasMaxLength(200)
                    .HasColumnName("cust_img");

                entity.Property(e => e.CustName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cust_name");

                entity.Property(e => e.CustPhone)
                    .HasMaxLength(20)
                    .HasColumnName("cust_phone");

                entity.Property(e => e.CustStatus).HasColumnName("cust_status");

                entity.Property(e => e.MetaData).HasColumnName("meta_data");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasMaxLength(50)
                    .HasColumnName("modified_date");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.Feedback1)
                    .HasMaxLength(250)
                    .HasColumnName("feedback");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.FinalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("final_price");

                entity.Property(e => e.MetaData).HasColumnName("meta_data");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.OrderDiscount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("order_discount");

                entity.Property(e => e.OrderPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("order_price");

                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(250)
                    .HasColumnName("remarks");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasKey(e => e.OrderLinesId);

                entity.ToTable("order_lines");

                entity.Property(e => e.OrderLinesId).HasColumnName("order_lines_id");

                entity.Property(e => e.CouriorName)
                    .HasMaxLength(50)
                    .HasColumnName("courior_name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.DiscountPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("discount_price");

                entity.Property(e => e.ExpectedDelieveryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expected_delievery_date");

                entity.Property(e => e.MetaData).HasColumnName("meta_data");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.SellerId).HasColumnName("seller_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TrackingNum)
                    .HasMaxLength(50)
                    .HasColumnName("tracking_num");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.DeliveryCharges)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("delivery_charges");

                entity.Property(e => e.DeliveryDays).HasColumnName("delivery_days");

                entity.Property(e => e.LongDesc).HasColumnName("long_desc");

                entity.Property(e => e.MetaData).HasColumnName("meta_data");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.NumOfViews).HasColumnName("num_of_views");

                entity.Property(e => e.ProductImg)
                    .HasMaxLength(1500)
                    .HasColumnName("product_img");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("product_price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.SellerId).HasColumnName("seller_id");

                entity.Property(e => e.SeoData).HasColumnName("seo_data");

                entity.Property(e => e.ShortDecsc)
                    .HasMaxLength(250)
                    .HasColumnName("short_decsc");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Ratting>(entity =>
            {
                entity.ToTable("ratting");

                entity.Property(e => e.RattingId).HasColumnName("ratting_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(250)
                    .HasColumnName("remarks");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.ToTable("sales");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.DiscountedPrice).HasColumnName("discounted_price");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.ToTable("seller");

                entity.Property(e => e.SellerId).HasColumnName("seller_id");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .HasColumnName("company_name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.LongDesc).HasColumnName("long_desc");

                entity.Property(e => e.MaritalStatus).HasColumnName("marital_status");

                entity.Property(e => e.MetaData).HasColumnName("meta_data");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.SellerAddress)
                    .HasMaxLength(250)
                    .HasColumnName("seller_address");

                entity.Property(e => e.SellerCnic).HasColumnName("seller_cnic");

                entity.Property(e => e.SellerDob)
                    .HasColumnType("datetime")
                    .HasColumnName("seller_dob");

                entity.Property(e => e.SellerEmail)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("seller_email");

                entity.Property(e => e.SellerGender)
                    .HasMaxLength(10)
                    .HasColumnName("seller_gender");

                entity.Property(e => e.SellerImg)
                    .HasMaxLength(200)
                    .HasColumnName("seller_img");

                entity.Property(e => e.SellerName)
                    .HasMaxLength(50)
                    .HasColumnName("seller_name");

                entity.Property(e => e.SellerPhone)
                    .HasMaxLength(20)
                    .HasColumnName("seller_phone");

                entity.Property(e => e.SeoData).HasColumnName("seo_data");

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(50)
                    .HasColumnName("short_desc");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.WebsiteUrl)
                    .HasMaxLength(50)
                    .HasColumnName("website_url");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.ToTable("subscriber");

                entity.Property(e => e.SubscriberId)
                    .ValueGeneratedNever()
                    .HasColumnName("subscriber_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubscriberEmail)
                    .HasMaxLength(50)
                    .HasColumnName("subscriber_email");

                entity.Property(e => e.SubscriberName)
                    .HasMaxLength(50)
                    .HasColumnName("subscriber_name");
            });

            modelBuilder.Entity<UserAdmin>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("user_admin");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.MetaData).HasColumnName("meta_data");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");

                entity.Property(e => e.Token)
                    .HasMaxLength(100)
                    .HasColumnName("token");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("wishlist");

                entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.MetaData).HasColumnName("meta_data");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.StaffAddress)
                    .HasMaxLength(250)
                    .HasColumnName("staff_address");

                entity.Property(e => e.StaffDob)
                    .HasColumnType("datetime")
                    .HasColumnName("staff_dob");

                entity.Property(e => e.StaffEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("staff_email");

                entity.Property(e => e.StaffImage)
                    .HasMaxLength(200)
                    .HasColumnName("staff_image");

                entity.Property(e => e.StaffName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("staff_name");

                entity.Property(e => e.StaffPhone).HasColumnName("staff_phone");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SystemUserId).HasColumnName("system_user_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
