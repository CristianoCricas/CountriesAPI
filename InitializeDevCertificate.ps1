#Create DEV certificate
dotnet dev-certs https -ep "src\CountriesAPIcert.pfx"  -p cricas123
dotnet dev-certs https --trust

