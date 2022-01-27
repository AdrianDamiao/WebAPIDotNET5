<template>
  <div>
  <div class="home">
    <img alt="Library logo" src="../../public/logo.png">
    <h1>{{ 'Work in Progress' }}</h1>
    <p>
    Projeto front-end da API desenvolvida em .NET5.0 no
    curso de C# do <a :href="githubAndre">André Darcie</a><br>
    Para mais detalhes sobre a API, acesse o <a :href="githubAdrian">repositório do github</a>.
    </p>
  </div>
  <div align="bottom">
    <p>Agradecimentos especiais ao <a :href="githubGeovani">Geovani Darcie</a>.</p>
  </div>
  </div>
</template>

<script>
export default {
  name: 'Home',

  data() {
    return {
      githubAndre: 'https://github.com/andredarcie',
      githubAdrian: 'https://github.com/AdrianDamiao/WebAPIDotNET5',
      githubGeovani: 'https://github.com/GeovaniDarcie',
    };
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
