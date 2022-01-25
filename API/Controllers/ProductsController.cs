using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //Attribute , Route, derive from
    [ApiController]
    [Route("api/[controller])")] //api/[controller]?
    public class ProductsController : ControllerBase
    {
        //add constructor : ctor tab
        //specify the StoreContext into ()
        //Initialize the field from parameter (Create and assign property)
        //To use dependency injection to get StoreContext here (access to database) 
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet] //Http method?, I'm going to return a list of products from this method
                  //specify the type of result i am returning 

        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
            //Asynchronous code.. because multi-thread(?) from many cunsumers the request might be blocked.
            //use the different thread.. ?
            //more scalable and easy to handle many more concurrent requests than if i leave the codes like this
            //1) async after public then insert task<> to asynchonize the task
            //2) take care of warning "await" inside of variable and ToList is not async version list ->ToListAsync()

            /* var products = await context.Products.ToListAsync(); 
            return Ok(products);
            Simple way of returning .=> return await context. ....*/

        }

        [HttpGet("{id}")]  //parenthese()/curly brackets{}/specify "id" parameter i am going to from the root 
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        //After changing the controller codes, should dotnet watch run again in terminal
        //In Swagger, there should be two additional endpoints, one for a list of products, the other for individual product with {id} passed as a parameter
        //returned in Json response that i can see the web browser...?_?
    }
}