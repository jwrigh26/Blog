import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import path from "path";

console.log("ImportMeta", import.meta.dirname);

export default defineConfig({
  plugins: [vue()],
  server: {
    port: 5173,
    strictPort: true,
    cors: true, // Enable ASP.NET Core (localhost) to fetch HMR scripts directly
    hrm: {
      host: "localhost",
    },
  },
  build: {
    outDir: "wwwroot/dist",
    emptyOutDir: true,
    manifest: true,
    rolldownOptions: {
      input: {
        "island-timezone": path.resolve(
          import.meta.dirname,
          "src/islands/timezone.ts",
        ),
        "island-ble-chunker": path.resolve(
          import.meta.dirname,
          "src/islands/ble-chunker.ts",
        ),
        "app-game": path.resolve(import.meta.dirname, "src/apps/game/main.ts"),
      },
    },
  },
});
