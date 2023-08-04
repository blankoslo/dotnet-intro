#!/bin/bash
#
docker stop todoapi-ef-db
docker rm todoapi-ef-db
docker run --name todoapi-ef-db \
-p 7070:5432 \
-v pgdata:/var/lib/postgresql/ef-data \
-e POSTGRES_USER=admin \
-e POSTGRES_PASSWORD=password1338 \
-d postgres
