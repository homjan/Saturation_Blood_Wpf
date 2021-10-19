# Saturation_Blood_Wpf


Детскопное приложение для расчета степени оксигенации крови человека, исходя из данных о прохождении света длиной 650 нм и 900 нм через ткани

# Функциональность.
Детскопное приложение (WPF), выполненное с использованием паттерна MVVM, для расчета степени оксигенации крови человека, исходя из данных о прохождении света длиной 650 нм и 900 нм через ткани. Приложение позволяет рассчитывать, выводить на экран (с использованием библиотеки Livecharts) и записывать в файл данные о насыщении крови кислородом. Расчет максимумов и минимумов реализован тремя различными способами с использованием внешней регулировки по амплитуде производной:
 - Прямой поиск по сигналу и его производной.
 - Использование нейронной сети.
 - Поиск с использованием статистики положения особых точек

Расчет степени оксигенации проводится на основе положения максимумов и минимумов пульсовых циклов ФПГ, получаемого из оцифрованного сигнала от 2 светодиодов (650 нм и 900 нм), разделяемого на полную и усиленную переменную составляющую. В приложении предусмотрена возможность предварительной обработки данных:

 - многократное сглаживание по 7 точкам.
 - расчет постоянных составляющих (с малой амплитудой), используя данные высокоамплитудного переменного сигнала.
 - инвертирование постоянных составляющих.
 - Просмотр результатов с 10-секундным шагом.

# Реализация и запуск
Реализация выполнена на языке C#. В файле test.txt приведена пятиминутная запись фотопретизмограммы, сделанной при задержки дыхания обследуемым. В папке Release - финальная сборка проекта. Запускать через exe-файл "Saturation_Blood_WPF"
Также приложен сгенерированный BMP-файл с зависимостью насыщения от времени.

# Требования
NET Framework 4.5. На записи должны присутствовать 4 сигнала ФПГ: постоянная и переменная составляющая для сигнала от светодиода с 650 нм и с 900 нм. Для корректного определения степени оксигенации число пульсовых циклов во всех составляющих должно быть приблизительно одинаковым.
