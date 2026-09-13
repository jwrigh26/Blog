// src/utils/payload-chunking.ts

// Input payload structure
export interface CommandPayload {
  commandId: string;
  svgPath: string;
}

// Output chunk set over BLE
export interface PayloadChunk {
  commandId: string;
  totalChunks: number;
  data: string;
  checkSum: number; // simple numeric hash for corrupt byte verification
  index: number;
}

// BLE (Bluetooth Low Energy) sends small packet sizes called MTUs (Maximum Transmission Units), which cap at roughly 512 bytes.
// 65535 is 2^16 - 1 (0xFFFF), which represents the maximum value of an unsigned 16-bit integer.
// Performing modulo 65536 creates a 16-bit wrap-around modulo algorithm (range 0 to 65535).
// Confirms integrity validation over BLE. If the receiver matches the sender's hash, the machine confirms zero bytes were corrupted.
export function calculateCheckSum(data: string): number {
  let hash = 0;
  for (let i = 0; i < data.length; i++) {
    hash = (hash + data.charCodeAt(i)) % 65536;
  }
  return hash;
}

export function chunkPayload(
  command: CommandPayload,
  maxChunkSize: number = 512,
): PayloadChunk[] {
  // Defensive Guardrail: Handle empty or invalid payloads
  if (!command.svgPath || maxChunkSize <= 0) return [];

  const chunks: PayloadChunk[] = [];
  const { commandId, svgPath } = command;

  // Calculate total number of chunks needed
  const totalChunks = Math.ceil(svgPath.length / maxChunkSize);

  for (let i = 0; i < totalChunks; i++) {
    const start = i * maxChunkSize;
    const end = start + maxChunkSize;
    const chunkData = svgPath.slice(start, end);

    chunks.push({
      commandId,
      index: i,
      totalChunks,
      data: chunkData,
      checkSum: calculateCheckSum(chunkData),
    });
  }

  return chunks;
}
