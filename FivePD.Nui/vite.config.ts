import { defineConfig, loadEnv } from 'vite';
import vue from '@vitejs/plugin-vue';
import Components from 'unplugin-vue-components/vite';
import * as path from 'path';

export default ({ mode }) => {

  process.env = {...process.env, ...loadEnv(mode, process.cwd())};

  return defineConfig({
    base: '',
    build: {
      target: 'es2015',
      outDir: process.env.VITE_OUTDIR
    },
    plugins: [
      vue(),
      Components({
        extensions: ['vue', 'md'],
        include: [/\.vue$/, /\.vue\?vue/, /\.md$/],
        dts: 'src/components.d.ts',
      }),
    ],
    resolve: {
      // Path resolving works with the values listed here, instead of the ones
      // that are in the tsconfig.json. To suppress errors in VSCode,
      // put these paths in the tsconfig.json as well.
      alias: {
        '@/': `${path.resolve(__dirname, 'src')}/`,
        '@utils': `${path.resolve(__dirname, 'src/utils')}/`,
        '@interfaces': `${path.resolve(__dirname, 'src/interfaces')}/`,
        '@store': `${path.resolve(__dirname, 'src/store')}/`,
        '@directives': `${path.resolve(__dirname, 'src/directives')}/`,
        '@hooks': `${path.resolve(__dirname, 'src/hooks')}/`,
        '@enums': `${path.resolve(__dirname, 'src/enums')}/`,
      }
    }
  })

}
