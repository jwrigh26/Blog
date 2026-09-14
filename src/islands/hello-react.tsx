import { createRoot } from "react-dom/client";
import { HelloReact } from "../components/HelloReact";

const mountNode = document.getElementById("island-react");

if (mountNode) {
  const root = createRoot(mountNode);
  root.render(<HelloReact />);
}
