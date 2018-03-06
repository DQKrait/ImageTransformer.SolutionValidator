Проверялка корректности оформления решений тестового задания на летнюю стажировку (https://docs.google.com/document/d/1yk5kt1oV4278LojQeZvSuSsiNdG0nNzcnIy29K0UXK8/edit).

Запускать так:
	SolutionValidator.exe <path-to-solution.zip> <path-to-msbuild.exe> 
Например:
	SolutionValidator.exe solution.zip "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\amd64\MSBuild.exe"

Алгоритм работы: 
1. Распаковать архив
2. Собрать решение
3. Запустить решение
4. Проверить, что сервис правильно выполняет все 4 типа преобразований для тестовой картинки
