# BYAS gRPC Sample
## Description
This repository contains application sample that uses gRPC. It consists of a client and server application.
## Services
### Counter Service
This service is an example of a streaming service. The client can call this service with an initial integer. The service will then stream the 10 next integers with a 1 second delay between each integer.
### TikTak Service
This service is an example of a request/response service. You can request "Tik" to this service and it will respond with "Tak". If you request something else, he will respond with "What are you talking about".
## Goal
This sample is used in the BYAS (Boost Your Architectur Skills) to explain gRPC.