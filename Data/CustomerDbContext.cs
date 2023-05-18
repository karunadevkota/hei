using Microsoft.EntityFrameworkCore;
using TAskHEI.Models;

namespace TAskHEI.Data
{
    public class CustomerDbContext: DbContext //DbContext  is a class provideed by Entity Framework Core that allows to interact with database
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext > options) :base(options)
        {
            // The constructor for the CustomerDbContext class.
            // It accepts an instance of DbContextOptions<CustomerDbContext> as a parameter.
            // This parameter is used to configure the database context options, such as the database provider, connection string, etc.
            // The base constructor is called passing the options to the base DbContext class.

        }
        public DbSet<Customer> Customers { get; set; }
        // The name of the property "Customers" will be used as the table name in the database.
        // By including this property in the DbContext class, Entity Framework Core will be able to create and manage the "Customers" table in the database.
        // The table name craeted by entity framework core will be Customers but in Customer class I have named the table name as tbl_Customer
        // This will mapp the Customers with tbl_Customer
        // tbl_Customer will be created in database 

    }
}
