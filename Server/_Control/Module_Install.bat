@echo off
title domi Package Install
echo - domi Package Installer -
echo [INFO] Node 무결성 검사
node -v
if %errorlevel% neq 0 (
   echo *****************************
    echo Node가 설치되어있지 않습니다.
    echo https://nodejs.org/ 에서 다운로드 하세요.
    echo *****************************
    pause
)

@rem package.json 찾기 위해서 경로 뒤로 가야함
cd ../

echo [INFO] 패키지 설치 시작
npm install
pause