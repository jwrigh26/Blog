<!-- src/components/BleChunkerIsland.vue -->
<template>
  <div class="ble-chunker-card">
    <h3>BLE Chunk Inspector</h3>
    <p class="subtitle">
      Simulate SVG payload transmission over Bluetooth MTUs
    </p>

    <div class="control-group">
      <label for="mtu-slider"
        >MTU Chunk Size: <strong>{{ maxChunkSize }} bytes</strong></label
      >
      <input
        id="mtu-slider"
        type="range"
        min="16"
        max="512"
        step="16"
        v-model.number="maxChunkSize"
      />
    </div>

    <div class="control-group">
      <label for="svg-input">SVG Path Data:</label>

      <textarea
        id="svg-input"
        v-model="svgPathInput"
        rows="3"
        placeholder="Enter SVG path commands..."
      ></textarea>
    </div>

    <div class="stats-bar">
      <span
        >Total Length: <strong>{{ svgPathInput.length }} chars</strong></span
      >
      <span
        >Total Chunks: <strong>{{ chunks.length }}</strong></span
      >
    </div>

    <div class="chunk-list">
      <h4>Payload Chunks</h4>
      <div v-for="chunk in chunks" :key="chunk.index" class="chunk-item">
        <div class="chunk-header">
          <span class="badge"
            >Chunk #{{ chunk.index + 1 }} / {{ chunk.totalChunks }}</span
          >
          <span class="checksum"
            >Checksum:
            <code
              >0x{{
                chunk.checkSum.toString(16).toUpperCase().padStart(4, "0")
              }}</code
            ></span
          >
        </div>
        <div class="chunk-body">
          <code>{{ chunk.data }}</code>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";
import { chunkPayload, type PayloadChunk } from "../utils/payload-chunking";

const maxChunkSize = ref<number>(64);
const svgPathInput = ref<string>(
  "M150 0 L75 200 L225 200 Z M10 80 L290 80 L50 240 L150 10 L250 240 Z",
);

const chunks = computed<PayloadChunk[]>(() => {
  return chunkPayload(
    {
      commandId: "cmd-ble-" + Date.now().toString(36),
      svgPath: svgPathInput.value,
    },
    maxChunkSize.value,
  );
});
</script>

<style scoped>
.ble-chunker-card {
  border: 2px solid #3b82f6;
  border-radius: 8px;
  padding: 16px;
  background-color: #1e293b;
  color: #f8fafc;
  max-width: 480px;
  font-family: system-ui, sans-serif;
  margin-top: 16px;
}
h3 {
  margin: 0 0 4px 0;
  color: #60a5fa;
}
.subtitle {
  margin: 0 0 16px 0;
  font-size: 0.85rem;
  color: #94a3b8;
}
.control-group {
  display: flex;
  flex-direction: column;
  gap: 6px;
  margin-bottom: 12px;
}
label {
  font-size: 0.9rem;
}
input[type="range"] {
  width: 100%;
}
textarea {
  width: 100%;
  background-color: #0f172a;
  border: 1px solid #475569;
  color: #38bdf8;
  border-radius: 4px;
  padding: 8px;
  font-family: monospace;
  box-sizing: border-box;
}
.stats-bar {
  display: flex;
  justify-content: space-between;
  font-size: 0.85rem;
  background-color: #0f172a;
  padding: 8px 12px;
  border-radius: 4px;
  margin-bottom: 12px;
}
.chunk-list {
  max-height: 220px;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 8px;
}
h4 {
  margin: 0 0 4px 0;
  font-size: 0.9rem;
  color: #cbd5e1;
}
.chunk-item {
  background-color: #0f172a;
  border: 1px solid #334155;
  border-radius: 4px;
  padding: 8px;
}
.chunk-header {
  display: flex;
  justify-content: space-between;
  font-size: 0.75rem;
  margin-bottom: 4px;
}
.badge {
  background-color: #2563eb;
  color: #fff;
  padding: 2px 6px;
  border-radius: 3px;
}
.checksum code {
  color: #4ade80;
}
.chunk-body code {
  font-size: 0.8rem;
  color: #f1f5f9;
  word-break: break-all;
}
</style>
