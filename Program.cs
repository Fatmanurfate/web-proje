var builder = WebApplication.CreateBuilder(args);

// MVC ve Session servislerini ekle
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // <--- BU SATIR YENİ EKLENDİ

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // <--- BU SATIR YENİ EKLENDİ (Sıralama önemli!)

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Açılış sayfası Login olsun

app.Run();