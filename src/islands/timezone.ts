import { createApp } from "vue";
import TimezoneClock from "../components/TimezoneClock.vue";

const mountNode = document.getElementById("island-timezone");

if (mountNode) {
  createApp(TimezoneClock).mount(mountNode);
}
