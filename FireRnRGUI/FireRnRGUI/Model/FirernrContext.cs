namespace FireRnRGUI.Model
{
    public partial class FirernrContext : DbContext
    {
        public FirernrContext()
        {
        }

        public FirernrContext(DbContextOptions<FirernrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Amenity> Amenities { get; set; } = null!;
        public virtual DbSet<AviliabilityRate> AviliabilityRates { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Property> Properties { get; set; } = null!;
        public virtual DbSet<PropertyAmenityOwner> PropertyAmenityOwners { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseLazyLoadingProxies().UseMySql("server=localhost;database=firernr;user=root;password=;treattinyasboolean=true", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.4-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.ToTable("amenity");

                entity.HasIndex(e => e.Amenity1, "amenity")
                    .IsUnique();

                entity.Property(e => e.AmenityId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("amenityID");

                entity.Property(e => e.Amenity1)
                    .HasMaxLength(50)
                    .HasColumnName("amenity");
            });

            modelBuilder.Entity<AviliabilityRate>(entity =>
            {
                entity.HasKey(e => e.RentalId)
                    .HasName("PRIMARY");

                entity.ToTable("aviliabilityRate");

                entity.HasIndex(e => e.PropertyId, "FK_rental_property");

                entity.Property(e => e.RentalId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("rentalID");

                entity.Property(e => e.AvailableFrom).HasColumnName("availableFrom");

                entity.Property(e => e.AvailableUntil).HasColumnName("availableUntil");

                entity.Property(e => e.DailyRate)
                    .HasColumnType("decimal(20,2) unsigned")
                    .HasColumnName("dailyRate");

                entity.Property(e => e.PropertyId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("propertyID");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.AviliabilityRates)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rental_property");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("booking");

                entity.HasIndex(e => e.PropertyId, "FK_booking_property");

                entity.HasIndex(e => e.UserId, "FK_booking_user");

                entity.Property(e => e.BookingId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("bookingID");

                entity.Property(e => e.BookedFrom).HasColumnName("bookedFrom");

                entity.Property(e => e.BookedUntil).HasColumnName("bookedUntil");

                entity.Property(e => e.BookingConfirmation)
                    .HasColumnType("tinyint(1) unsigned")
                    .HasColumnName("bookingConfirmation");

                entity.Property(e => e.BookingExpires)
                    .HasColumnType("int(1) unsigned")
                    .HasColumnName("bookingExpires")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.BookingTime).HasColumnName("bookingTime");

                entity.Property(e => e.PaymentConfirmation)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("paymentConfirmation");

                entity.Property(e => e.PaymentDate).HasColumnName("paymentDate");

                entity.Property(e => e.PaymentMethod)
                    .HasColumnType("enum('Credit Card','Paypal')")
                    .HasColumnName("paymentMethod");

                entity.Property(e => e.PropertyId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("propertyID");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("UserID")
                    .HasComment("Guest user");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_booking_property");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_booking_user");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.HasIndex(e => e.BookingId, "FK_message_booking");

                entity.HasIndex(e => e.SenderId, "FK_message_user");

                entity.HasIndex(e => e.ToUserId, "FK_message_user_2");

                entity.Property(e => e.MessageId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("messageID");

                entity.Property(e => e.BookingId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("bookingID");

                entity.Property(e => e.MessageContent)
                    .HasColumnType("text")
                    .HasColumnName("messageContent");

                entity.Property(e => e.MessageTime)
                    .HasColumnType("datetime")
                    .HasColumnName("messageTime");

                entity.Property(e => e.SenderId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("senderID");

                entity.Property(e => e.ToUserId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("toUserID");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_message_booking");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_message_user");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.MessageToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_message_user_2");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("property");

                entity.HasIndex(e => new { e.PropertyName, e.PropertyAddrNo, e.PropertyAddrStreet, e.PropertyAddrCity, e.PropertyAddrProv, e.PropertyAddrCountry, e.PropertyPostalCode }, "propertyName_propertyAddress")
                    .IsUnique();

                entity.Property(e => e.PropertyId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("propertyID");

                entity.Property(e => e.NoOfRooms)
                    .HasColumnType("int(2) unsigned")
                    .HasColumnName("noOfRooms");

                entity.Property(e => e.PropertyAddrCity)
                    .HasMaxLength(50)
                    .HasColumnName("propertyAddrCity");

                entity.Property(e => e.PropertyAddrCountry)
                    .HasMaxLength(50)
                    .HasColumnName("propertyAddrCountry");

                entity.Property(e => e.PropertyAddrNo)
                    .HasMaxLength(50)
                    .HasColumnName("propertyAddrNo");

                entity.Property(e => e.PropertyAddrProv)
                    .HasMaxLength(50)
                    .HasColumnName("propertyAddrProv");

                entity.Property(e => e.PropertyAddrStreet)
                    .HasMaxLength(50)
                    .HasColumnName("propertyAddrStreet");

                entity.Property(e => e.PropertyDescription)
                    .HasColumnType("text")
                    .HasColumnName("propertyDescription");

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(50)
                    .HasColumnName("propertyName");

                entity.Property(e => e.PropertyPhoto)
                    .HasMaxLength(50)
                    .HasColumnName("propertyPhoto");

                entity.Property(e => e.PropertyPostalCode)
                    .HasMaxLength(7)
                    .HasColumnName("propertyPostalCode");

                entity.Property(e => e.PropertyType)
                    .HasColumnType("enum('Apartment','House')")
                    .HasColumnName("propertyType");
            });

            modelBuilder.Entity<PropertyAmenityOwner>(entity =>
            {
                entity.HasKey(e => e.PropertyAmenityId)
                    .HasName("PRIMARY");

                entity.ToTable("property_amenity_owner");

                entity.HasComment("Junction table");

                entity.HasIndex(e => e.AmenityId, "FK_property_amenity_amenity");

                entity.HasIndex(e => e.PropertyId, "FK_property_amenity_property");

                entity.HasIndex(e => e.UserId, "FK_property_amenity_user");

                entity.Property(e => e.PropertyAmenityId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("property_amenityID");

                entity.Property(e => e.AmenityId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("amenityID");

                entity.Property(e => e.PropertyId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("propertyID");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("userID")
                    .HasComment("Owner of the property");

                entity.HasOne(d => d.Amenity)
                    .WithMany(p => p.PropertyAmenityOwners)
                    .HasForeignKey(d => d.AmenityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_property_amenity_amenity");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertyAmenityOwners)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_property_amenity_property");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PropertyAmenityOwners)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_property_amenity_user");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("rating");

                entity.HasIndex(e => e.BookingId, "FK_rating_booking");

                entity.HasIndex(e => e.UserId, "FK_rating_user");

                entity.HasIndex(e => new { e.UserId, e.BookingId, e.UserRateAs }, "userID_bookingID_userRateAs")
                    .IsUnique();

                entity.Property(e => e.RatingId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("ratingID");

                entity.Property(e => e.BookingId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("bookingID");

                entity.Property(e => e.UserComment)
                    .HasColumnType("text")
                    .HasColumnName("userComment");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("userID");

                entity.Property(e => e.UserRate)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("userRate");

                entity.Property(e => e.UserRateAs)
                    .HasColumnType("enum('USER','OWNER')")
                    .HasColumnName("userRateAs");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rating_booking");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rating_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.UserName, "userName")
                    .IsUnique();

                entity.HasIndex(e => new { e.UserFirstName, e.UserLastName, e.MailAddrStreetNo, e.MailAddrStreet, e.MailAddrCity, e.MailAddrProv, e.MailAddrCountry, e.MailPostalCode }, "userName_userAddress")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("userID");

                entity.Property(e => e.AccountActivation)
                    .HasColumnType("tinyint(1) unsigned")
                    .HasColumnName("accountActivation")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AccumulatedRate)
                    .HasColumnType("decimal(4,2) unsigned")
                    .HasColumnName("accumulatedRate");

                entity.Property(e => e.DateJoined).HasColumnName("dateJoined");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .HasColumnType("enum('M','F')")
                    .HasColumnName("gender");

                entity.Property(e => e.LastLogin).HasColumnName("lastLogin");

                entity.Property(e => e.LastReservation).HasColumnName("lastReservation");

                entity.Property(e => e.MailAddrCity)
                    .HasMaxLength(30)
                    .HasColumnName("mailAddrCity");

                entity.Property(e => e.MailAddrCountry)
                    .HasMaxLength(30)
                    .HasColumnName("mailAddrCountry");

                entity.Property(e => e.MailAddrProv)
                    .HasMaxLength(30)
                    .HasColumnName("mailAddrProv");

                entity.Property(e => e.MailAddrStreet)
                    .HasMaxLength(50)
                    .HasColumnName("mailAddrStreet");

                entity.Property(e => e.MailAddrStreetNo)
                    .HasMaxLength(10)
                    .HasColumnName("mailAddrStreetNo");

                entity.Property(e => e.MailPostalCode)
                    .HasMaxLength(7)
                    .HasColumnName("mailPostalCode");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(20)
                    .HasColumnName("phoneNo");

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .HasColumnName("photo");

                entity.Property(e => e.UserFirstName)
                    .HasMaxLength(15)
                    .HasColumnName("userFirstName");

                entity.Property(e => e.UserLastName)
                    .HasMaxLength(15)
                    .HasColumnName("userLastName");

                entity.Property(e => e.UserName)
                    .HasMaxLength(15)
                    .HasColumnName("userName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
