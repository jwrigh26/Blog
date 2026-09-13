<template>
  <div class="clock-card">
    <h3>Tokoy Tracker</h3>
    <p>{{ tokoyTime }}</p>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";

const tokoyTime = ref("");
let timer: ReturnType<typeof setInterval> | undefined = undefined;

const updateTime = () => {
  tokoyTime.value = new Intl.DateTimeFormat("en-US", {
    timeZone: "Asia/Tokoy",
    dateStyle: "medium",
    timeStyle: "medium",
  }).format(new Date());
};

onMounted(() => {
  updateTime();
  timer = setInterval(updateTime, 1000);
});

onUnmounted(() => clearInterval(timer));
</script>

<style scoped>
.clock-card {
  border: 2px solid #333;
  padding: 1rem;
  border-radius: 8px;
  background-color: #f4f4f9;
  display: inline-block;
}
h3 {
  margin: 0 0 0.5rem 0;
  color: #2c3e50;
}
</style>
