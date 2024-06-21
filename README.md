# SocialMedia3" 
API DOCUMENTO SOCIAL MEDIA

# CREAR IMAGEN
docker build -t social-media-app -f Dockerfile .

# LEVANTAR CONTENEDOR 1
docker run --name social-media-app --network my-net -p 8000:80 -p 5002:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=5002 -e ASPNETCORE_Kestrel__Certificates__Default__Password=123456-a -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v C:\Users\rchecalla_dev\.aspnet\https:/https/ social-media-app

# LEVANTAR CONTENEDOR 2
 docker run --name social-media-app -p 5002:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=5002 -e ASPNETCORE_Kestrel__Certificates__Default__Password=123456-a -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v C:\Users\RICARDO\.aspnet\https:/https/ social-media-app