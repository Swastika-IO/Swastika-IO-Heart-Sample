using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.OData.Query;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
    [Route("Posts")]
    public class PostsController : Controller
    {
        // GET: Posts
        [Route("")]
        [Route("{pageSize}/{pageIndex}")]
        public async Task<IActionResult> Index(int? pageSize = 10, int? pageIndex = 0)
        {
            var getPosts = await PostViewModel.Repository.GetModelListAsync("CreatedDate", OrderByDirection.Descending, pageSize, pageIndex);

            return View(getPosts.Data);
        }

        // GET: Posts/Details/5
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var getPost = await PostViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (!getPost.IsSucceed)
            {
                return NotFound();
            }
            return View(getPost.Data);
        }

        // GET: Posts/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                await post.SaveModelAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }


        // GET: Posts/Edit/5
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var getPost = await PostViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (!getPost.IsSucceed)
            {
                return NotFound();
            }
            return View(getPost.Data);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PostViewModel post)
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
                    if (!PostExists(post.Id))
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

        // GET: Posts/Delete/5
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getPost = await PostViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (!getPost.IsSucceed)
            {
                return NotFound();
            }

            return View(getPost.Data);
        }

        // POST: Posts/Delete/5
        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var getPost = await PostViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (getPost.IsSucceed)
            {
                await getPost.Data.RemoveModelAsync(true);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(string id)
        {
            return PostViewModel.Repository.CheckIsExists(e => e.Id == id);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("_AddComment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _AddComment(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                await comment.SaveModelAsync();
                return RedirectToAction(nameof(Details), new { Id = comment.PostId });
            }
            return View(comment);
        }

        // POST: Posts/Delete/5
        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var getPost = await CommentViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (getPost.IsSucceed)
            {
                await getPost.Data.RemoveModelAsync();
                return RedirectToAction(nameof(Details), new { Id = getPost.Data.PostId });
            }
            return NotFound();
        }
    }
}