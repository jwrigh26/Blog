import { createApp } from "vue";
import TimezoneClock from "../components/TimezoneClock.vue";

const mountNode = document.getElementById("island-vue");

if (mountNode) {
  createApp(TimezoneClock).mount(mountNode);
}
