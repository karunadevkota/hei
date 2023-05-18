using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TAskHEI.Data;
using TAskHEI.Models;

namespace TAskHEI.Controllers
{
    public class CustomerController : Controller //inherits from the Controller class
    {
        private readonly CustomerDbContext _context; //database context  
        public CustomerController(CustomerDbContext context) //Dependency injection
        {
            _context = context;
        }
        public IActionResult Index(string searchQuery, int? page) //Index is for handling requests to Index view
            //searchQuery represents the serach query entered by user 
            //page represnts the current page 
        {
            int pageSize = 5; // shows up to 5 data per each page 
            int pageNumber = page ?? 1; // default page is 1

            var customers = _context.Customers.ToList(); //reterives all  customers from table 

            if (!string.IsNullOrEmpty(searchQuery))
            {
                customers = customers.Where(c => c.Name.ToLower().Contains(searchQuery.ToLower()) || c.Email.ToLower().Contains(searchQuery.ToLower())).ToList();
                // Tolower method  is for case insensitive comparision 
                // search for the name or email enter by the user and pass it to the customers variable 
            }

            var pagedCustomers = new PagedList.PagedList<Customer>(customers, pageNumber, pageSize);
            return View(pagedCustomers);// pass the page number to the view 
        }
        public IActionResult Create() //Get Method  to display the form for craeting new user
        {
            var customer = new Customer(); //Crate an instance of class Customer 
            return View(customer); // pass the object  to the Create view 

        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid) //checks for the validation definde in the customer Model Class
                // only the valid data is savedd
            {
                _context.Customers.Add(customer); // Add the customer object to the database 
                _context.SaveChanges(); //Save the changes
                return RedirectToAction("Index"); //redirect the user to index view page afer sucessfully adding new customer 
            }
            return View(customer); // if the model is not valid 
        }


        public IActionResult Edit(int customerId) //passes the customerId
        {
            var customer = _context.Customers.Find(customerId); //retrieve the customer wit taht id from the databse 
            if (customer == null)
            {
                return NotFound(); //if cno customer data 
            }
            return View(customer); //pass the customer object  to the Edit View page 
        }

        [HttpPost] // post method is for form submission 
        public IActionResult Edit(int customerId, Customer customer)
        {
            if (customerId != customer.CustomerID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Customers.Update(customer); //upadte the database wit the changes made on the specified id 
                _context.SaveChanges();  //save the changes 
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Delete(int customerId)
        {
            var customer = _context.Customers.Find(customerId);  // finds the specified customerid 
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);  // pass the object for the Delete view
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                _context.Customers.Remove(customer); //remove the data of that customerid from the table  .
                _context.SaveChanges();
                return RedirectToAction("Index"); ///redirect the user to the Index view page after successful deleteion 
            }

            // if there occurs any error it will execute the catch block
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the customer."); // erreor message 
                return View("Delete", customer); //display the Delete view page 
            }

        }
        public IActionResult Details(int customerId) // display the detail of the specified customer 
        {
            var customer = _context.Customers.Find(customerId); //retrieve the data of that id 

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer); /// pass the customer object for the detail vieww
        }


    }
}
