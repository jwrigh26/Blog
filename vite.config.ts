import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import react from "@vitejs/plugin-react";
import path from "path";

const blogRoot = import.meta.dirname;

// Module Driven Request Architecture:
// Vite acts as an ES MOdule (ESM) HTTP server rather than a framework-bound compiler.
// When ASP.NET Core Razor pages requests are made:
// <script type="module" src="http://localhost:5173/.../react.island.tsx">
//
// The browser makes a standard HTTP request to localhost 5173.
// Vite runs the requested file path through it's configured plugin chain:
//  - .vue hits @vite/plugin-vue
//  - .jsx/.tsx files hit @vite/plguin-react
//
//  Becuase transformation happens on demand per file, Vite doesn't care that vue() and react() plugins live in the same vite.config.ts
//
//  Diving Deeper:
//  Independent HMR WebSocket Streams:
//  Vite uses a single WebSocket connection on port 5173 for Hot Module Reloading (HMR).
//  This means Vite sends messages only to the framework being edited. React and Vue won't intefere with each other. Vite FTW.
//
//  For Production:
//  During `npm run build`, Vite uses Rollup to resolve all specified entry points.
//  Rollup, rolls up the shared dependencies, compiles React and Vue components into separate optimized JS chunks, and outputs them into
//  `wwwroot/dist`.

export default defineConfig({
  plugins: [
    vue(),
    react({
      include: /\.(js|jsx|ts|tsx)$/,
    }),
  ],
  server: {
    port: 5173,
    strictPort: true,
    cors: true, // Enable ASP.NET Core (localhost) to fetch HMR scripts directly
    hmr: {
      host: "localhost",
    },
  },
  build: {
    outDir: "wwwroot/dist",
    emptyOutDir: true,
    manifest: true,
    rolldownOptions: {
      input: {
        "island-timezone": path.resolve(blogRoot, "src/islands/timezone.ts"),
        "island-ble-chunker": path.resolve(
          blogRoot,
          "src/islands/ble-chunker.ts",
        ),
        "island-react": path.resolve(blogRoot, "src/islands/hello-react.ts"),
        "app-game": path.resolve(blogRoot, "src/apps/game/main.ts"),
      },
      output: {
        entryFileNames: "[name].js",
      },
    },
  },
});
