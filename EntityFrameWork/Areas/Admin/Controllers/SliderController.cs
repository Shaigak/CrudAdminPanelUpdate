using EntityFrameWork.Data;
using EntityFrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderController : Controller

{

    private readonly AppDbContext _Context;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public SliderController(AppDbContext Context, IWebHostEnvironment webHostEnvironment)
    {
        _Context = Context;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        IEnumerable<Slider> sliders = await _Context.Sliders.Where(m => !m.SoftDelete).ToListAsync();
        return View(sliders);
    }


    [HttpGet]
    public async Task<IActionResult> Detail(int? id)
    {

        if (id == null) return BadRequest();

        Slider? slider = await _Context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

        if (slider is null) return NotFound();

        return View(slider);

    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Slider slider)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View();  // Eger sekil secmeyibse View return elesin 
            }

            if (!slider.Photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
            {
                ModelState.AddModelError("Photo", "File type must be image");

                return View();

            }

            if (slider.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Image Size must be max 200kb");  // Sekilin 200 kbde boyukduse error mesajini cixartsin 
                return View();
            }





            string fileName = Guid.NewGuid().ToString() + " " + slider.Photo.FileName; // herdefe yeni ad duzeldirik . 

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img",fileName); // root duzeldirik . 

            using (FileStream stream=new FileStream(path, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
            {
              await slider.Photo.CopyToAsync(stream);  
            }

            slider.Image = fileName;  // sliderin imagenisini photoya beraberlesdirek 

            await _Context.Sliders.AddAsync(slider);  // gelen slideri bazaya save edek 

            await _Context.SaveChangesAsync();  // databazaya sava edek 

            return RedirectToAction(nameof(Index)); // Indexe redirect edek 




        }
        catch (Exception)
        {

            throw;
        }
      
    }



    [HttpPost]

    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Delete(int? id)
    {

        try
        {
            if (id == null) return BadRequest();
            Slider slider = await _Context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            
            if (slider is null) return NotFound();
            
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.Image); // root duzeldirik . 

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _Context.Sliders.Remove(slider);

            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {

            throw;
        }


    }



    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return BadRequest();

        Slider? slider = await _Context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

        if (slider is null) return NotFound();



        return View(slider);
    }










}

