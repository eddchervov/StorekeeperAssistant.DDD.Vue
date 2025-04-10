<b>«Помощник кладовщика»</b><br><br>

<a target="_blank" href="http://46.180.95.110:1315/">DEMO</a>

Данное приложение предназначено для перемещение товаров между складами компании, получение на склад извне, расход со склада. <br><br>
<p class="card-subtitle mb-1">
  <b>Что имеется в системе.</b>
</p>
<ul>
  <li>В системе существуют следующие справочники: «Склады компании» и «Номенклатуры».</li>
  <li>Перемещаемый товар(или ТМЦ) это совокупность номенклатуры и кол-ва.</li>
  <li>В одном перемещении между складами(приходе/расходе) может быть несколько ТМЦ, при этом номенклатуры не повторяются.</li>
  <li>Страница со списком всех перемещений(время, откуда, куда)</li>
  <li>Страница создания перемещения, приход/расход (откуда, куда, список перемещаемых ТМЦ)</li>
  <li>Страница - отчет по остаткам с выбором склада и времени, на которое отображать остатки.</li>
  <li>Приложение умеет инициализировать базу при первом запуске (не менее 3-х складов и не менее 7-ми номенклатур).</li>
  <li>В приложении есть энд-поинт на заполнение данными.</li>
</ul>

<br>

<p class="card-subtitle mb-1">
  <b>Стек технологий.</b>
</p>
<ul>
  <li>.NET 9.0.</li>
  <li><b>Server: </b> Web Api (DDD, CQS, REST API).</li>
  <li><b>Client: </b> Vue 2 + Babel + TypeScript + Router + Vuex + Eslink + доп. библиотеки
    (<a target="_blank" href="https://github.com/sagalbot/vue-select">vue-select</a>, 
    <a target="_blank" href="https://github.com/chronotruck/vue-ctk-date-time-picker">vue-ctk-date-time-picker</a>,
    <a target="_blank" href="https://github.com/lokyoung/vuejs-paginate">vuejs-paginate</a>).
  </li>
  <li><b>База данных: </b> MsSql</li>
  <li><b>ORM: </b> Entity Framework Core 9, Dapper</li>
</ul>
