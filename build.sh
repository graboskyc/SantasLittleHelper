#!/bin/bash

source .env
echo "Using conn string starting ${MDBCONNSTR:0:18}..."

datehash=`date | md5sum | cut -d" " -f1`
abbrvhash=${datehash: -8}

echo
echo "+======================"
echo "| START: Santas Little Helper"
echo "+======================"
echo

echo 
echo "Building container using tag ${abbrvhash}"
echo
docker build -t graboskyc/slh:${abbrvhash} .

EXITCODE=$?

echo
echo "SLH: Starting container"
echo

if [ $EXITCODE -eq 0 ]
    then

    docker stop slh
    docker rm slh
    docker run -t -i -d -p 9999:8080 --name slh -e "MDBCONNSTR=${MDBCONNSTR}" --restart unless-stopped graboskyc/slh:${abbrvhash}

    echo
    echo "+================================"
    echo "| END: Santas Little Helper"
    echo "+================================"
    echo

else
    echo
    echo "+================================"
    echo "| ERROR: Build failed"
    echo "+================================"
    echo
fi