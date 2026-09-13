<template>
  <div class="game-container">
    <h2>Isometric Travel Viewer</h2>
    <canvas ref="canvasRef" width="320" height="240"></canvas>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";

const canvasRef = ref<HTMLCanvasElement | null>(null);

onMounted(() => {
  const canvas = canvasRef.value;
  if (!canvas) return;

  const ctx = canvas.getContext("2d");
  if (!ctx) return;

  // Disable smoothing for sharp pixel art[cite: 1]
  ctx.imageSmoothingEnabled = false;

  let x = 160;
  let y = 100;

  const render = () => {
    ctx.fillStyle = "#1e1e24";
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    // Render an isometric pixel art tile
    ctx.fillStyle = "#4ea8de";
    ctx.beginPath();
    ctx.moveTo(x, y);
    ctx.lineTo(x + 20, y + 10);
    ctx.lineTo(x, y + 20);
    ctx.lineTo(x - 20, y + 10);
    ctx.closePath();
    ctx.fill();

    requestAnimationFrame(render);
  };

  render();
});
</script>

<style scoped>
.game-container {
  text-align: center;
}
canvas {
  border: 4px solid #1e1e24;
  image-rendering: pixelated;
  width: 640px;
  height: 480px;
}
</style>
