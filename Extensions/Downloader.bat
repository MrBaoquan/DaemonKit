@echo off && color A
setlocal EnableDelayedExpansion
echo ������Ҫ���ص��ļ���ַ
set /p fileUrl=:
for /r %%f in (%fileUrl%) do (
	set saveTo=%userprofile%\downloads\%%~nxf
)

curl -L %fileUrl%>%saveTo%
echo �ļ���������:%saveTo%
explorer.exe /select,%saveTo%
ping 127.0.0.1 -n 3 >nul