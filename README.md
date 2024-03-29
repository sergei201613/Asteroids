# Asteroids

Поиграть можно по ссылке:
https://yandex.ru/games/?app=193138

Все файлы с кодом можно найти в данном каталоге: https://github.com/sergei201613/Asteroids/tree/main/Assets/Asteroids/Scripts

Аналог игры Asteroids (Arcade Game: Asteroids (1979 Atari)).

Игровой мир представляет собой часть космоса, в которой пользователь управляет космическим кораблем, смотря на него сверху. Корабль перемещается в области экрана и выходя за его пределы появляется с противоположной стороны.

В сцене присутствуют также дрейфующие астероиды и НЛО. Первые имеют разнообразный в определенных пределах размер, кроме того, они могут перемещаться в любом направлении с различной скоростью. При столкновении с астероидами космический корабль взрывается.

## Астероиды
Астероид имеет три состояния:
1) Крупный
2) Средний
3) Малый

В начале уровня астероиды всегда крупные.
При старте игры появляется 2 астероида.
После уничтожения всех астероидов - они появляются снова через 2 секунды, но на этот раз их на 1 астероид больше, чем до этого.

При появлении скорость выбирается случайно в определенных пределах.

При столкновении с пулей - Крупный или Средний астероид разламывается на 2 части, образуя астероиды поменьше.
Новые астероиды разлетаются в стороны, в направлении движения разрушенного астероида + 45 градусов и -45 градусов.
Скорость новых астероидов одинаковая, но значение случайное.

При столкновении с игроком или НЛО - астероид полностью уничтожается, независимо от его размеров.

## НЛО
Появляется раз в 20-40 секунд с момента уничтожения последнего или начала игры.

Пролетает слева направо или справа налево (случайно), позиция по вертикали выбирается так же случайно, но не ближе чем 20% к границам экрана сверху или снизу.

Преодолевает экран примерно за 10 секунд.
Во время перемещения стреляет по игроку со случайной частотой в определенных пределах.

## Корабль игрока
Обладает следующими характеристиками: максимальная скорость, скорость поворота, ускорение.
Движение корабля имеет инерцию.
Трение отсутствует, поэтому скорость корабля не снижается.

Перемещение: корабль может только вращаться и ускорятся.

При спауне имеет неуязвимость в течении 3 секунд, полностью функционален в этот момент. Во время неуязвимости корабль появляется и исчезает с частотой 2 раза в секунду.

Игрок может стрелять, максимальноя частота зависит от корабля.
Запас пуль бесконечен.

## Мир и его границы
При выходе за пределы экрана астероиды, игрок и НЛО появляются с противоположной стороны.

## Интерфейс
### В игре
На экране отображается кол-во жизней и очков.

Очки начисляются так:
+20 за крупный астероид
+50 за средний
+100 за маленький
+200 за НЛО

### В меню
При входе в меню игра ставится на паузу.
Игровой интерфейс отображается (очки, кол-во жизней).

В меню расположены кнопки:
1) Продолжить (если игра не начата)
2) Новая игра
3) “Управление: клавиатура” / “Управление: клавиатура + мышь” (меняет текст по клику, работает как переключатель)
4) Выход

## Управление
Есть две схемы управления (меняется в меню).

ESC - пауза, меню.

### Клавиатура
Стрелки или WAD - поворот + ускорение.
Пробел - выстрел.

### Мышь + клавиатура
Курсор мыши - поворот - корабль игрока поворачивается за курсором мыши. Но у корабля есть скорость поворота - поэтому, корабль поворачивается не моментально.
Кнопка W или стрелка вверх или правая кнопка мыши - ускорение.
Пробел или левая кнопка мыши - выстрел.

## Графика взята на сайте:
https://kenney.nl/assets
