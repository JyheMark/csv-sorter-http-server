# Http CSV sort server
A HTTP server for sorting CSV files ascendingly by column number.

## Directions
Compile and run. Runs on port 8080.

POST csv file to http://localhost:8080/sort/{n} where {n} is the column number to sort on.

Server responds 200OK with the sorted CSV in the body.

## TODO
- [x] Accept comma as delimiter
- [x] Accept semicolon as delimiter
- [ ] Accept tab as delimiter
- [ ] Incoming CSV validation