# Net9 JwtAuthentication

## CURL

### Weather forecast

curl --request GET --url https://localhost:7247/api/weather-forecast

### Register

curl -X POST  "https://localhost:7247/api/account/register" -H "Content-Type: application/json" -H "origin: https://localhost:7247" -d "{\"email\" : \"your-username@gmail1.com\", \"password\" : \"YourP@ssword1!\",\"confirmpassword\" : \"YourP@ssword1!\"}"

### LOGIN

curl -X POST "https://localhost:7247/api/account/login" -H "accept: */*" -H "Content-Type: application/json" -H "origin: https://localhost:7247" -d "{ \"email\" : \"your-username@gmail1.com\", \"password\" : \"YourP@ssword1!\" }"

### Secure Weather forecast

curl -X GET "https://localhost:7247/api/secure-weather-forecast" -H "Authorization: Bearer {AccessToken}"
