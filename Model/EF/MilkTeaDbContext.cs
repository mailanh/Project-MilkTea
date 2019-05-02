namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MilkTeaDbContext : DbContext
    {
        public MilkTeaDbContext()
            : base("name=MilkTeaDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;//this is line to be added
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<AmountOfStone> AmountOfStones { get; set; }
        public virtual DbSet<AmountOfSugar> AmountOfSugars { get; set; }
        public virtual DbSet<Bill_Import> Bill_Import { get; set; }
        public virtual DbSet<Bill_ImportDetail> Bill_ImportDetail { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Emlpoyee> Emlpoyees { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<NewCategory> NewCategories { get; set; }
        public virtual DbSet<Nutritionalinformation> Nutritionalinformations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionProduct> PromotionProducts { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Splouse> Splice { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<About>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<About>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<About>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<AmountOfStone>()
                .Property(e => e.AmountStoneName)
                .IsFixedLength();

            modelBuilder.Entity<AmountOfSugar>()
                .Property(e => e.AmountSugarName)
                .IsFixedLength();

            modelBuilder.Entity<Content>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Emlpoyee>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Emlpoyee>()
                .Property(e => e.Gmail)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Target)
                .IsFixedLength();

            modelBuilder.Entity<NewCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<NewCategory>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<NewCategory>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Nutritionalinformation>()
                .Property(e => e.Fat)
                .IsUnicode(false);

            modelBuilder.Entity<Nutritionalinformation>()
                .Property(e => e.Energy)
                .IsUnicode(false);

            modelBuilder.Entity<Nutritionalinformation>()
                .Property(e => e.VitaminB1)
                .IsUnicode(false);

            modelBuilder.Entity<Nutritionalinformation>()
                .Property(e => e.Calo)
                .IsUnicode(false);

            modelBuilder.Entity<Nutritionalinformation>()
                .Property(e => e.Canxi)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.AmountOfStoneID)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.AmountOfSugarID)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.SizeID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.IsHot)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.IsNew)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Size>()
                .Property(e => e.SizeID)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
               .HasRequired(e => e.Supplier)
               .WithMany(e => e.Products)
               .HasForeignKey(e => e.SupplierID);

            modelBuilder.Entity<Product>()
               .HasRequired(e => e.ProductCategory)
               .WithMany(e => e.Products)
               .HasForeignKey(e => e.CategoryID);

            //modelBuilder.Entity<Order>()
            //   .HasRequired(e => e.Customer)
            //   .WithMany(e => e.Orders)
            //   .HasForeignKey(e => e.CustomerID);

        }
    }
}
