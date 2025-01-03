# rira_question
##  پیاده‌سازی  CRUD بر اساس مدل پرسنل و استفاده از Protocol Buffers (gRPC) به جای REST، 

### ۱. ایجاد مدل داده
### ۲. تعریف فایل Proto
### ۳. ایجاد سرویس gRPC
### ۴. ثبت سرویس در برنامه
### ۵. مدیریت خطاها
     1- Use Repository Pattern
     2- FluentValidation
     3- Middleware for Integrating error handling
     4- Use Serilog & log for any operation in logs/log-{datetime}.txt
     5- Use customized status code
     6- SQLite for simple database in data/person.db
     7- Use client for testing services
    
## ساخت برنامه های مقیاس پذیر و با کارایی بالا با gRPC در NET 8.
سرویس gRPC، مخفف Google Remote Procedure Call، یک چارچوب با کارایی بالا است که بر روی پروتکل HTTP/2 ساخته شده است.gRPC از بافرهای پروتکل برای تعریف API سرویس استفاده می کند. بافرهای پروتکل مکانیزمی هستند که از نظر زبانی خنثی، بی‌طرفانه و قابل توسعه برای سریال‌سازی داده‌های ساخت‌یافته هستند. این امر توسعه سرویس‌های gRPC را که می‌تواند با انواع کلاینت‌ها و سرورها استفاده شود، آسان می‌کند. 

## 4 الگوی ارتباطی ضروری gRPC
          Server to client streaming
          Client to server streaming
          Bi-directional streaming
          Unary (Unary RPC no streaming)
          
##  مدل Unary RPC 
یک الگوی ارتباطی در gRPC است که در آن مشتری یک پیام درخواست واحد را به سرور ارسال می کند و سرور یک پیام پاسخ واحد را به مشتری ارسال می کند. این ساده‌ترین الگوی ارتباطی در gRPC است و برای برنامه‌هایی که مشتری باید یک درخواست را به سرور ارسال کند و یک پاسخ را دریافت کند، مناسب است

## فرمت های سریال سازی در gRPC
### انواع سریال سازی
          بافرهای پروتکل (زبان خنثی، روشی کارآمد برای تعریف ساختارهای داده)
          نوع JSON (قابل خواندن توسط انسان، آسان برای استفاده)
## بافرهای پروتکل
نوع Protocol Buffers فرمت سریال‌سازی پیش‌فرض و توصیه‌شده برای gRPC در NET Core است. این یک فرمت سریال سازی باینری با زبان آگنوستیک است که توسط Google توسعه یافته است. با protobuf، ساختار داده های خود را با استفاده از یک فایل .proto تعریف می کنید و پیام ها و فیلدهای آنها را مشخص می کنید. سپس کامپایلر protobuf کد قوی تایپ شده را در زبان مقصد شما (در این مورد C#) برای سریال سازی و deserialization تولید می کند.

نوع JSON در حالی که protobuf انتخاب پیشنهادی است، gRPC در NET Core از سریال‌سازی JSON نیز پشتیبانی می‌کند. JSON یک فرمت تبادل داده قابل خواندن توسط انسان است که به طور گسترده مورد استفاده قرار می گیرد. با سریال سازی JSON، ساختار پیام خود را با استفاده از اشیاء انتقال داده (DTOs) در سی شارپ تعریف می کنید که با ویژگی هایی مانند [DataContract] و [DataMember] حاشیه نویسی شده است. سپس سریال‌ساز JSON این اشیاء را برای ارتباط به فرمت JSON تبدیل می‌کند.


               منبع : https://dev.to/ahmedshahjr/building-scalable-and-high-performance-applications-with-grpc-in-net-8-4ffp
