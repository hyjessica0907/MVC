using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using X.PagedList;

namespace Project.Controllers
{
    public class ScoresController : Controller
    {
        private readonly CmsContext _context;
        public ScoresController(CmsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var grades = _context.TableGrade1121751.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                grades = grades.Where(g =>
                    g.StudentId.Contains(searchString) ||
                    g.Name.Contains(searchString) ||
                    g.Department.Contains(searchString));
            }

            var pagedList = await grades
                .OrderBy(g => g.StudentId)
                .ToPagedListAsync(pageNumber, pageSize);

            ViewData["CurrentFilter"] = searchString; // 傳回 View 顯示用
            return View(pagedList);
        }
        // 顯示新增表單
        public IActionResult Create()
        {
            return View();
        }

        // 處理表單送出
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }
        // 查看成績詳細資料
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var grade = await _context.TableGrade1121751.FindAsync(id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        // 編輯（GET）
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var grade = await _context.TableGrade1121751.FindAsync(id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        // 編輯（POST）
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Grade grade)
        {
            if (id != grade.StudentId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(grade);
        }

        // 刪除（GET）
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var grade = await _context.TableGrade1121751.FindAsync(id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        // 刪除（POST）
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var grade = await _context.TableGrade1121751.FindAsync(id);
            if (grade != null)
            {
                _context.TableGrade1121751.Remove(grade);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }



    }
}
