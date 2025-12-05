import java.util.*;
import java.util.function.Consumer;
import java.util.stream.Collectors;

/**
 * The Cosmos Symphony
 * * 이 프로그램은 현대적인 Java 문법을 사용하여 작은 우주를 시뮬레이션합니다.
 * - Java 14+ Records (데이터 불변성)
 * - Builder Pattern (객체 생성의 우아함)
 * - Stream API (선언적 데이터 처리)
 * - Polymorphism (다형성)
 */
public class CosmosSymphony {

    public static void main(String[] args) {
        // 1. 우주 생성 (Builder Pattern 사용)
        Galaxy milkyWay = Galaxy.builder()
                .name("Milky Way")
                .addBody(Star.of("Sun", 1.989E30, CelestialColor.YELLOW))
                .addBody(Planet.builder()
                        .name("Earth")
                        .mass(5.972E24)
                        .distanceFromStar(1.0) // 1 AU
                        .hasLife(true)
                        .build())
                .addBody(Planet.builder()
                        .name("Mars")
                        .mass(6.39E23)
                        .distanceFromStar(1.52)
                        .hasLife(false)
                        .build())
                .addBody(Planet.builder()
                        .name("Jupiter")
                        .mass(1.898E27)
                        .distanceFromStar(5.20)
                        .hasLife(false)
                        .build())
                .build();

        // 2. 시뮬레이션 실행
        System.out.println("🔭 Initializing Cosmos Simulation...");
        milkyWay.simulateTimePassage(5);

        // 3. 데이터 분석 (Stream API 활용)
        System.out.println("\n📊 Analysis Report:");
        milkyWay.analyzeLifePotential();
    }
}

// --- Domain Models & Enums ---

/**
 * 천체의 색상 타입 (Enum with properties)
 */
enum CelestialColor {
    YELLOW("✨"), BLUE("🔵"), RED("🔴"), GREY("⚪"), ORANGE("🟠");

    private final String icon;

    CelestialColor(String icon) {
        this.icon = icon;
    }

    public String getIcon() {
        return icon;
    }
}

/**
 * 2차원 좌표 (Java Record - 불변 데이터 객체)
 */
record Vector2D(double x, double y) {
    public double distanceTo(Vector2D other) {
        return Math.sqrt(Math.pow(this.x - other.x, 2) + Math.pow(this.y - other.y, 2));
    }
}

// --- Abstraction Layer ---

/**
 * 모든 천체의 기본이 되는 추상 클래스
 */
abstract class CelestialBody {
    protected final String name;
    protected final double mass;
    protected Vector2D position;

    protected CelestialBody(String name, double mass, Vector2D position) {
        this.name = name;
        this.mass = mass;
        this.position = position;
    }

    public abstract void update(double timeStep);
    public abstract String getStatus();

    public String getName() { return name; }
}

// --- Concrete Implementations ---

/**
 * 항성 (Star) - 움직이지 않는 중심점
 */
class Star extends CelestialBody {
    private final CelestialColor color;

    private Star(String name, double mass, CelestialColor color) {
        super(name, mass, new Vector2D(0, 0)); // 중심에 위치
        this.color = color;
    }

    // Static Factory Method
    public static Star of(String name, double mass, CelestialColor color) {
        return new Star(name, mass, color);
    }

    @Override
    public void update(double timeStep) {
        // 별은 움직이지 않고 에너지만 방출한다고 가정
    }

    @Override
    public String getStatus() {
        return String.format("%s [STAR] %s (Mass: %.2e kg) - Burning Bright", 
                color.getIcon(), name, mass);
    }
}

/**
 * 행성 (Planet) - 별 주위를 공전
 */
class Planet extends CelestialBody {
    private final double distanceFromStar;
    private final boolean hasLife;
    private double orbitalAngle; // 라디안

    private Planet(String name, double mass, double distanceFromStar, boolean hasLife) {
        super(name, mass, new Vector2D(distanceFromStar, 0));
        this.distanceFromStar = distanceFromStar;
        this.hasLife = hasLife;
        this.orbitalAngle = 0.0;
    }

    @Override
    public void update(double timeStep) {
        // 간단한 공전 시뮬레이션 (케플러 법칙 무시, 단순 회전)
        double orbitalSpeed = 1.0 / Math.sqrt(distanceFromStar); 
        this.orbitalAngle += orbitalSpeed * timeStep;

        double newX = Math.cos(orbitalAngle) * distanceFromStar;
        double newY = Math.sin(orbitalAngle) * distanceFromStar;
        this.position = new Vector2D(newX, newY);
    }

    @Override
    public String getStatus() {
        String lifeStatus = hasLife ? "🌿 Life Detected" : "💀 Barren";
        return String.format("🪐 [PLANET] %-10s | Pos: (%.2f, %.2f) | %s", 
                name, position.x(), position.y(), lifeStatus);
    }

    public boolean hasLife() { return hasLife; }

    // Inner Builder Class
    public static Builder builder() { return new Builder(); }

    public static class Builder {
        private String name;
        private double mass;
        private double distanceFromStar;
        private boolean hasLife;

        public Builder name(String name) { this.name = name; return this; }
        public Builder mass(double mass) { this.mass = mass; return this; }
        public Builder distanceFromStar(double dist) { this.distanceFromStar = dist; return this; }
        public Builder hasLife(boolean hasLife) { this.hasLife = hasLife; return this; }

        public Planet build() {
            return new Planet(name, mass, distanceFromStar, hasLife);
        }
    }
}

// --- System Manager ---

/**
 * 은하계 (Galaxy) - 천체들의 집합 관리자
 */
class Galaxy {
    private final String name;
    private final List<CelestialBody> bodies;

    private Galaxy(String name, List<CelestialBody> bodies) {
        this.name = name;
        this.bodies = bodies;
    }

    public void simulateTimePassage(int steps) {
        System.out.println("--- Starting Simulation for " + name + " ---");
        for (int i = 1; i <= steps; i++) {
            try {
                Thread.sleep(300); // 시각적 효과를 위한 지연
                updateUniverse(0.5);
                renderUniverse(i);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                System.err.println("Simulation interrupted: " + e.getMessage());
            }
        }
    }

    private void updateUniverse(double timeStep) {
        bodies.forEach(body -> body.update(timeStep));
    }

    private void renderUniverse(int step) {
        System.out.println("\n[Time Step " + step + "]");
        bodies.forEach(body -> System.out.println("  " + body.getStatus()));
    }

    public void analyzeLifePotential() {
        List<String> lifeBearingPlanets = bodies.stream()
                .filter(b -> b instanceof Planet)
                .map(b -> (Planet) b)
                .filter(Planet::hasLife)
                .map(CelestialBody::getName)
                .collect(Collectors.toList());

        if (lifeBearingPlanets.isEmpty()) {
            System.out.println("  ❌ No known life in this system.");
        } else {
            System.out.println("  ✅ Life found on: " + String.join(", ", lifeBearingPlanets));
        }
    }

    // Galaxy Builder
    public static GalaxyBuilder builder() { return new GalaxyBuilder(); }

    public static class GalaxyBuilder {
        private String name;
        private final List<CelestialBody> bodies = new ArrayList<>();

        public GalaxyBuilder name(String name) { this.name = name; return this; }
        public GalaxyBuilder addBody(CelestialBody body) { this.bodies.add(body); return this; }

        public Galaxy build() {
            if (name == null) name = "Unnamed Galaxy";
            return new Galaxy(name, bodies);
        }
    }
}
