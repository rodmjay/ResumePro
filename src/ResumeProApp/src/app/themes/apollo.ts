import { Theme } from '@primeng/themes';

const Apollo = {
  name: 'Apollo',
  primitive: {
    // Base color palettes from Aura theme
    blue: {
      50: '#f5f9ff',
      100: '#d0e1fd',
      200: '#abc9fb',
      300: '#85b2f9',
      400: '#609af8',
      500: '#3b82f6',
      600: '#326fd1',
      700: '#295bac',
      800: '#204887',
      900: '#183462'
    },
    green: {
      50: '#f4fcf7',
      100: '#caf1d8',
      200: '#a0e6ba',
      300: '#76db9b',
      400: '#4cd07d',
      500: '#22c55e',
      600: '#1da750',
      700: '#188a42',
      800: '#136c34',
      900: '#0e5127'
    },
    yellow: {
      50: '#fefbf3',
      100: '#faedc4',
      200: '#f6de95',
      300: '#f2d066',
      400: '#eec137',
      500: '#eab308',
      600: '#c79807',
      700: '#a47d06',
      800: '#816204',
      900: '#5e4803'
    },
    cyan: {
      50: '#f3fbfd',
      100: '#c3edf5',
      200: '#94dfee',
      300: '#65d1e6',
      400: '#35c3df',
      500: '#06b6d4',
      600: '#059bb4',
      700: '#047f94',
      800: '#036475',
      900: '#024955'
    },
    pink: {
      50: '#fef6fa',
      100: '#fad3e7',
      200: '#f7b0d3',
      300: '#f38ec0',
      400: '#f06bac',
      500: '#ec4899',
      600: '#c93d82',
      700: '#a5326b',
      800: '#822854',
      900: '#5e1d3d'
    },
    indigo: {
      50: '#f7f7fe',
      100: '#dadafc',
      200: '#bcbdf9',
      300: '#9ea0f6',
      400: '#8183f4',
      500: '#6366f1',
      600: '#5457cd',
      700: '#4547a9',
      800: '#363885',
      900: '#282960'
    },
    teal: {
      50: '#f3fbfb',
      100: '#c7eeea',
      200: '#9ae0d9',
      300: '#6dd3c8',
      400: '#41c5b7',
      500: '#14b8a6',
      600: '#119c8d',
      700: '#0e8074',
      800: '#0b655c',
      900: '#084a43'
    },
    orange: {
      50: '#fff8f3',
      100: '#feddc7',
      200: '#fcc39b',
      300: '#fba86f',
      400: '#fa8e42',
      500: '#f97316',
      600: '#d46213',
      700: '#ae510f',
      800: '#893f0c',
      900: '#642e09'
    },
    bluegray: {
      50: '#f7f8f9',
      100: '#dadee3',
      200: '#bcc3cd',
      300: '#9fa9b7',
      400: '#818ea1',
      500: '#64748b',
      600: '#556376',
      700: '#465161',
      800: '#37404c',
      900: '#282e38'
    },
    purple: {
      50: '#fbf7ff',
      100: '#ead6fd',
      200: '#d9b6fc',
      300: '#c996fa',
      400: '#b876f9',
      500: '#a855f7',
      600: '#8f48d2',
      700: '#763bac',
      800: '#5c2e87',
      900: '#432162'
    },
    red: {
      50: '#fff5f5',
      100: '#ffd0ce',
      200: '#ffaca7',
      300: '#ff8780',
      400: '#ff6259',
      500: '#ff3d32',
      600: '#d9342b',
      700: '#b32b23',
      800: '#8c221c',
      900: '#661814'
    },
    gray: {
      50: '#f9fafb',
      100: '#e5e7eb',
      200: '#d1d5db',
      300: '#9ca3af',
      400: '#6b7280',
      500: '#4b5563',
      600: '#374151',
      700: '#1f2937',
      800: '#111827',
      900: '#0f172a'
    }
  },
  semantic: {
    primary: {
      50: '#f5f9ff',
      100: '#d0e1fd',
      200: '#abc9fb',
      300: '#85b2f9',
      400: '#609af8',
      500: '#3b82f6',
      600: '#326fd1',
      700: '#295bac',
      800: '#204887',
      900: '#183462'
    },
    colorScheme: {
      light: {
        primary: {
          color: '{primary.500}',
          contrastColor: '#ffffff',
          hoverColor: '{primary.600}',
          activeColor: '{primary.700}'
        },
        highlight: {
          background: '{primary.50}',
          focusBackground: '{primary.100}',
          color: '{primary.700}',
          focusColor: '{primary.800}'
        }
      },
      dark: {
        primary: {
          color: '{primary.400}',
          contrastColor: '{surface.900}',
          hoverColor: '{primary.300}',
          activeColor: '{primary.200}'
        },
        highlight: {
          background: 'color-mix(in srgb, {primary.400}, transparent 84%)',
          focusBackground: 'color-mix(in srgb, {primary.400}, transparent 76%)',
          color: 'rgba(255,255,255,.87)',
          focusColor: 'rgba(255,255,255,.87)'
        }
      }
    }
  },
  options: {
    darkModeSelector: '.app-dark',
  }
};

export default Apollo;
