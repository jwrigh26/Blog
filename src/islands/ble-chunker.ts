// src/islands/ble-chunker.ts
import { createApp } from "vue";
import BleChunkerIsland from "../components/BleChunkerIsland.vue";

const mountNode = document.getElementById("island-ble-chunker");

if (mountNode) {
  createApp(BleChunkerIsland).mount(mountNode);
}
