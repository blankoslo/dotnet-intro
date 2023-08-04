#!/bin/bash
#
docker stop todoapi-dapper-db
docker rm todoapi-dapper-db
docker run --name todoapi-dapper-db \
-p 8070:5432 \
-v pgdata:/var/lib/postgresql/dapper-data \
-e POSTGRES_USER=admin \
-e POSTGRES_PASSWORD=password1338 \
-d postgres
