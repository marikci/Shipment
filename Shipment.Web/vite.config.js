import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  filenameHashing: true,
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./app', import.meta.url))
    }
  },
  build: {
    outDir: './wwwroot/dist',
    rollupOptions: {
      output: {
        entryFileNames: 'app.js',
        assetFileNames: 'app.css',
      }
    },
    commonjsOptions: {
      esmExternals: true
    },
  }
})
