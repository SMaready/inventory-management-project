import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default {
  server: {
    proxy: {
      "/weatherforecast": {
        target: "http://localhost:5190",
        changeOrigin: true,
        secure: false
      }
    }
  }
}

