import type React from 'react';
import { cn } from '@/lib/utils';

interface BrutalistBoxProps {
  children: React.ReactNode;
  className?: string;
  background?: string;
}

export function BrutalistBox({
  children,
  className,
  background = 'bg-background',
}: BrutalistBoxProps) {
  return (
    <div
      className="w-96 px-8 py-4 bg-white border-4 border-black shadow-[3px_3px_0px_rgba(0,0,0,1)] grid place-content-center"
    >
      {children}
    </div>
  );
}
