<script lang="ts" setup>
import { ref } from 'vue';

type Forecasts = {
  date: string;
  temperatureC: string;
  temperatureF: string;
  summary: string;
}[];

const loading = ref(true);
const post = ref<Forecasts | null>(null);

fetch('/api/weather')
  .then((r) => r.json())
  .then((json) => {
    post.value = json as Forecasts;
    loading.value = false;
  });
</script>

<template>
  <div class="weather-component">
    <h1>Weather Forecast</h1>
    <p>This component demonstrates fetching data from the server.</p>

    <div v-if="loading" class="loading">
      Loading... Please refresh once the ASP.NET backend has started.
    </div>

    <div v-if="post" class="content">
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="forecast in post" :key="forecast.date">
            <td>{{ forecast.date }}</td>
            <td>{{ forecast.temperatureC }}</td>
            <td>{{ forecast.temperatureF }}</td>
            <td>{{ forecast.summary }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
th {
  font-weight: bold;
}
tr:nth-child(even) {
  background: #f8f8f8;
}

tr:nth-child(odd) {
  background: #e4e4e4;
}

th,
td {
  padding-left: 0.5rem;
  padding-right: 0.5rem;
}

.weather-component {
  text-align: center;
}

table {
  margin-left: auto;
  margin-right: auto;
  color: #000;
}
</style>
