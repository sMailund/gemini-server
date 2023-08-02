this https://www.baeldung.com/openssl-self-signed-cert

and this https://nodeployfriday.com/posts/self-signed-cert/


openssl genrsa -des3 -out domain.key 2048

openssl x509 -text -noout -in domain.crt

openssl req -new -nodes -x509 -days 365 -keyout domain.key -out domain.crt -config ./openssl.cnf

openssl pkcs12 -export -in domain.crt -inkey domain.key -out mycert.pfx                      
