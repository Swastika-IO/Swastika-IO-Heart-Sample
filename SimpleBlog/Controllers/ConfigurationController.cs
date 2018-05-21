using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.OData.Query;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
    [Authorize]
    [Route("Configurations")]
    public class ConfigurationsController : Controller
    {
        // GET: Configurations
        [Route("")]
        [Route("{pageSize}/{pageIndex}")]
        public async Task<IActionResult> Index(int? pageSize = 10, int? pageIndex = 0)
        {
            var getConfigurations = await ConfigurationViewModel.Repository.GetModelListAsync("CreatedDate", OrderByDirection.Descending, pageSize, pageIndex);

            return View(getConfigurations.Data);
        }

        // GET: Configurations/Details/5
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var getConfiguration = await ConfigurationViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (!getConfiguration.IsSucceed)
            {
                return NotFound();
            }
            return View(getConfiguration.Data);
        }

        // GET: Configurations/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configurations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConfigurationViewModel post)
        {
            if (ModelState.IsValid)
            {
                await post.SaveModelAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }


        // GET: Configurations/Edit/5
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var getConfiguration = await ConfigurationViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (!getConfiguration.IsSucceed)
            {
                return NotFound();
            }
            return View(getConfiguration.Data);
        }

        // POST: Configurations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ConfigurationViewModel post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await post.SaveModelAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigurationExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Configurations/Delete/5
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getConfiguration = await ConfigurationViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (!getConfiguration.IsSucceed)
            {
                return NotFound();
            }

            return View(getConfiguration.Data);
        }

        // POST: Configurations/Delete/5
        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var getConfiguration = await ConfigurationViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (getConfiguration.IsSucceed)
            {
                await getConfiguration.Data.RemoveModelAsync(true);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigurationExists(string id)
        {
            return ConfigurationViewModel.Repository.CheckIsExists(e => e.Id == id);
        }
    }
}