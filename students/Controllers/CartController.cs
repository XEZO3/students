using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using students.Data;
using students.Models;
using System.Security.Claims;

namespace students.Controllers
{
    public class CartController : Controller
    {
        private readonly StudentContext _context;
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;
        public CartController(StudentContext context, SignInManager<userAuth> signInManager, UserManager<userAuth> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _UserManager = userManager;
        }
        [Authorize(Roles ="User")]
        public async Task<IActionResult> Index(bool isajax)
        {
            var userId = _UserManager.GetUserId(User);
            
            
            var cartitem = _context.Cart.Include(x => x.cartItem).ThenInclude(x => x.Courses).FirstOrDefault(x => x.UserId == userId)?.cartItem.ToList();
            cartitem = (cartitem == null )? new List<cartItem>() : cartitem;
            if (isajax)
            {

                return PartialView("_partialCartItem", cartitem);
            }
            return View(cartitem);
        }
        [Authorize(Roles = "User")]
        public int AddToCart(int Id)
        {
            var userId = _UserManager.GetUserId(User);
            var userCourses = _context.UserCourses.FirstOrDefault(x=>x.Id == Id && x.UserId == userId );
            if (userCourses==null) {
                
                var cart = _context.Cart.Include(x => x.cartItem).FirstOrDefault(x => x.UserId == userId);
                if (cart == null)
                {
                    cart = new cart()
                    {
                        UserId = userId,
                        CreatDate = DateTime.Now,
                    };
                    cart.cartItem = new List<cartItem>() {
                new cartItem(){ CoursesId = Id,cartId = cart.Id}
                };
                    _context.Cart.Add(cart);
                    _context.SaveChanges();
                }
                else
                {
                    if (cart.cartItem.FirstOrDefault(x => x.CoursesId == Id) == null)
                    {
                        cartItem cartitem = new cartItem() { CoursesId = Id, cartId = cart.Id };
                        _context.cartItem.Add(cartitem);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return 0;
                    }

                }
                int count = _context.Cart.Include(x => x.cartItem).FirstOrDefault(x => x.UserId == userId).cartItem.Count();
                HttpContext.Session.SetInt32("cart", count);
                return count;
            }
            else {
                return 33;
            }
        }
        public async Task<List<dynamic>> RemoveFromCart(int Id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = _context.cartItem.FirstOrDefault(x => x.CoursesId == Id);
            if (cart == null)
            {
                return new List<dynamic> { "empty", 0 };
            }
            else
            {
                _context.cartItem.Remove(cart);
                await _context.SaveChangesAsync();
                var count = _context.Cart.Include(x => x.cartItem).FirstOrDefault(x => x.UserId == userId)?.cartItem.Count();
                HttpContext.Session.SetInt32("cart", (int)count);
                return new List<dynamic> { "full", count };
            }


        }
        public IActionResult checkOut()
        {
            var cartItem = _context.Cart.Include(x => x.cartItem).ThenInclude(x => x.Courses).FirstOrDefault(x => x.UserId == _UserManager.GetUserId(User)).cartItem.ToList();
            var total = cartItem.Sum(x => x.Courses.Price);
            

            foreach (var item in cartItem)
            {
                var usercourses = new UserCourses() { UserId = _UserManager.GetUserId(User), CoursesId = item.CoursesId };
                _context.UserCourses.Add(usercourses);
                _context.cartItem.Remove(item);
            }
            HttpContext.Session.SetInt32("cart", 0);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
 }
