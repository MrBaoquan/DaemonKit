@echo off

%1 mshta vbscript:CreateObject("Shell.Application").ShellExecute("cmd.exe","/c %~s0 ::","","runas",1)(window.close)&&exit
cd /d "%~dp0"


setlocal EnableDelayedExpansion
color A

echo ��ѡ������������
set _id=0
for /f "skip=3 tokens=1*" %%i in ('netsh interface show interface') DO (
	set /a _id+=1
	echo [!_id!] %%j
)

:SelectNetAdapter
set /p _input=:

set _id=0
for /f "skip=3 tokens=3*" %%i in ('netsh interface show interface') DO (
	set /a _id+=1
	set name_!_id!=%%j
)

if "!name_%_input%!" EQU "" (
	echo ��������,����������
	echo.
	goto SelectNetAdapter
)

echo.
set address="!name_%_input%!"
echo ����ѡ��%address%
echo ����IPV4��ַ
set /p IP=:

echo ������������
set /p MASK=:

echo �������ص�ַ
set /p GATEWAY=:

echo ����DNS:
set /p DNS1=:

netsh interface ipv4 set address name=%address% static addr=%IP% mask=%MASK% gateway=%GATEWAY%
netsh interface ipv4 set dns %address% static %DNS1%

echo IP�������
echo ���ڶ�ȡIP��Ϣ:
ping 127.0.0.1 -n 5 >nul

netsh interface ip show addresses %address%

pause