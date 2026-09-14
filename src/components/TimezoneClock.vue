<template>
  <div class="clock-card">
    <h3>Tokyo Time</h3>
    <p>{{ tokyoTime }}</p>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";

const tokyoTime = ref("");
let timer: ReturnType<typeof setInterval> | undefined = undefined;

const updateTime = () => {
  tokyoTime.value = new Intl.DateTimeFormat("en-US", {
    timeZone: "Asia/Tokyo",
    year: "numeric",
    month: "short",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    second: "2-digit",
    hour12: true,
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
