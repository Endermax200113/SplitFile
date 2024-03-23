# SplitFile
SplitFile - это программа, которая позволяет дробить огромные файлы на несколько частей. Помимо этого, программа может соединять мелкие файлы в один единый файл.
## Версии
### v0.0.4
- Добавлена новая обработка исключений, когда нет аргументов событии в кнопке;
- Был добавлен класс `ButtonDirectory.cs` в папке `GUI`, отвечающее за папку;
- Был добавлен класс `ButtonFile.cs` в папке `GUI`, отвечающее за файл;
- Был добавлен класс `PanelDirectory.cs` в папке `GUI`, которые отображают кнопки, отвечающие за папки и файлы;
- Все коды из `FormMain.cs` были перекинуты в другие файлы в папке `GUI`;
- Были запрограммированы кнопки, которые следуются по папкам.
### v0.0.3
- Был добавлен вспомогательный панель для управления файлов;
- Были деактивированы элементы для управления файлом;
- Была запрогромирована форма загрузки, во время которой загружает начальные кнопки;
- Была добавлена обработка исключений во время возникновения ошибки кнопки.
### v0.0.2
- Были изменены цвета оформления окна;
- Добавлена боковая панель с вкладками `Дробление`, `Слияние` и `Настройки`;
- Были добавлены вспомогательные панели;
- Добавлена нижняя правая кнопка "Добавить файл для дробления";
- Добавлена кнопка "Дробить файл";
- Были добавлены информации о дроблении файлов.
### v0.0.1
- Добавлен пакет файлов `MaterialSkin.2` для оформления окон в стиле "Material Design";
- Вставлен необходимый код в `Form1.cs`;
- Были изменены некоторые свойства в файле `Form1.cs`;
- Файл `Form1.cs` был перемещён в папку `Forms` и был переименован в `FormMain.cs`.