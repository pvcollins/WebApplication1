using WebApplication1.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
namespace WebApplication1.Controllers
{
    public class AccountsController : ODataController
    {
        AccountsContext db = new AccountsContext();
        private bool AccountExists(int key)
        {
            return db.Accounts.Any(p => p.AccountId == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // QUERY/GET methods
        [EnableQuery]
        public IQueryable<Accounts> Get()
        {
            return db.Accounts;
        }
        [EnableQuery]
        public SingleResult<Accounts> Get([FromODataUri] int key)
        {
            IQueryable<Accounts> result = db.Accounts.Where(p => p.AccountId == key);
            return SingleResult.Create(result);
        }

        // ADD methods
        public async Task<IHttpActionResult> Post(Accounts account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Accounts.Add(account);
            await db.SaveChangesAsync();
            return Created(account);
        }

        // UPDATE methods
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Accounts> account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Accounts.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            account.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Accounts update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.AccountId)
            {
                return BadRequest();
            }
            db.Entry(update).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(update);
        }
        // DELETE methods
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var account = await db.Accounts.FindAsync(key);
            if (account == null)
            {
                return NotFound();
            }
            db.Accounts.Remove(account);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // End of CRUD methods
    }
}