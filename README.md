# signalr-lab

docker build -t case-alert-hub:1.0 -f signalr-hub/Dockerfile .

docker run --name case-alert-hub -it --rm -p 32774:32775 case-alert-hub:1.0
