import { FaqSection } from "../components/FaqSection";
import { FeaturesSection } from "../components/FeatureSection";
import Hero from "../components/Hero";
import { LogoCarousel } from "../components/LogoCarousel";
import { PricingSection } from "../components/PricingSection";
import { TeamSection } from "../components/TeamSection";
import { TestimonialSection } from "../components/TestimonialSection";

export default function LandingPage() {
  return (
    <main className="min-h-screen bg-white dark:bg-black">
      <div className="mx-auto max-w-7xl px-6 pt-40">
        <Hero />
        <LogoCarousel />
        <FeaturesSection />
        <TeamSection />
        <TestimonialSection />
        <PricingSection />
        <FaqSection />
      </div>
    </main>
  );
}
