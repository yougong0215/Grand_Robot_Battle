@echo off
title domi Package Install
echo - domi Package Installer -
echo [INFO] Node ���Ἲ �˻�
node -v
if %errorlevel% neq 0 (
   echo *****************************
    echo Node�� ��ġ�Ǿ����� �ʽ��ϴ�.
    echo https://nodejs.org/ ���� �ٿ�ε� �ϼ���.
    echo *****************************
    pause
)

@rem package.json ã�� ���ؼ� ��� �ڷ� ������
cd ../

echo [INFO] ��Ű�� ��ġ ����
npm install
pause